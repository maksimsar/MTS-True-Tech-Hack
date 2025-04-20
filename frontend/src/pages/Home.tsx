// src/pages/Home.tsx
import { useState } from "react";
import ChatWindow from "../features/ChatWindow";
import JsonEditor from "../features/JsonEditor";
import HistoryPanel from "../features/HistoryPanel";
import ThemeToggle from "../components/ThemeToggle";

interface SchemaResponse {
  id: number;
  name: string;
  jsonSchema: string;
}

interface ChatMessageResponse {
  text: string;
  isFromUser: boolean;
  timestamp: string;
}

export default function Home() {
  const API = import.meta.env.VITE_API_URL!; // http://localhost:5074/api

  const [schemaId, setSchemaId] = useState<number | null>(null);
  const [messages, setMessages] = useState<string[]>([]);
  const [input, setInput] = useState("");
  const [jsonSchema, setJsonSchema] = useState("{}");
  const [history, setHistory] = useState<string[]>([]);

  const handleSend = async () => {
    if (!input.trim()) return;

    // 1) добавляем сообщение пользователя в чат
    setMessages(m => [...m, `You: ${input}`]);
    setHistory(h => [...h, `Пользователь: ${input}`]);

    try {
      // A. Если схемы ещё нет — создаём и сразу комментируем
      if (schemaId === null) {
        // Создаём схему
        const resSchema = await fetch(`${API}/schemas`, {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify({
            userId: 1,
            name: input,
            description: input
          })
        });
        if (!resSchema.ok) throw new Error(await resSchema.text());
        const { id, jsonSchema: js } = (await resSchema.json()) as SchemaResponse;

        setSchemaId(id);
        setJsonSchema(js);

        // Шлём сразу же комментарий GPT на основе сгенеренной схемы
        const resChat = await fetch(`${API}/schemas/${id}/chat`, {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify({
            message: input,
            history: []  // пустая история
          })
        });
        if (!resChat.ok) throw new Error(await resChat.text());
        const { text } = (await resChat.json()) as ChatMessageResponse;

        // В чат окно выводим реальный ответ GPT
        setMessages(m => [...m, `AI: ${text}`]);
        setHistory(h => [...h, `AI: ${text}`]);
      }
      // B. Если схема уже есть — обычный чат
      else {
        const resChat = await fetch(`${API}/schemas/${schemaId}/chat`, {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify({
            message: input,
            history: history.map(entry => ({
              text: entry.replace(/^Пользователь: |^AI: /, ""),
              isFromUser: entry.startsWith("Пользователь:"),
              timestamp: new Date().toISOString()
            }))
          })
        });
        if (!resChat.ok) throw new Error(await resChat.text());
        const { text } = (await resChat.json()) as ChatMessageResponse;

        setMessages(m => [...m, `AI: ${text}`]);
        setHistory(h => [...h, `AI: ${text}`]);
      }
    } catch (e: any) {
      console.error("API error:", e);
      setMessages(m => [...m, `Ошибка: ${e.message}`]);
      setHistory(h => [...h, `Ошибка: ${e.message}`]);
    } finally {
      setInput("");
    }
  };

  return (
    <div
      className="h-screen flex flex-col"
      style={{ backgroundColor: "var(--bg)", color: "var(--fg)" }}
    >
      {/* Header */}
      <header className="flex-none flex justify-between items-center px-6 py-6 md:py-8 max-w-[1400px] w-full mx-auto">
        <div>
          <h1 className="text-3xl md:text-4xl font-semibold tracking-tight text-[var(--accent)]">
            AI Schema Assistant
          </h1>
          <p className="text-sm text-zinc-400 mt-1">
            Инструмент для генерации и редактирования JSON‑схем
          </p>
        </div>
        <ThemeToggle />
      </header>

      {/* Main content */}
      <main
        className="flex-1 flex overflow-hidden px-6 max-w-[1400px] w-full mx-auto gap-4 pb-4"
        style={{ minHeight: 0 }}
      >
        {/* Левая половина: чат */}
        <div className="w-1/2 h-full flex flex-col min-h-0">
          <ChatWindow
            messages={messages}
            input={input}
            setInput={setInput}
            handleSend={handleSend}
          />
        </div>

        {/* Правая половина: JSON‑редактор + история */}
        <div className="w-1/2 h-full flex flex-col min-h-0 gap-4">
          <div className="h-1/2 flex flex-col min-h-0">
            <JsonEditor json={jsonSchema} setJson={setJsonSchema} />
          </div>
          <div className="h-1/2 flex flex-col min-h-0">
            <HistoryPanel history={history} />
          </div>
        </div>
      </main>
    </div>
  );
}
