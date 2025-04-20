// src/pages/Home.tsx
import { useState } from "react";
import ChatWindow from "../features/ChatWindow";
import JsonEditor from "../features/JsonEditor";
import HistoryPanel from "../features/HistoryPanel";
import ThemeToggle from "../components/ThemeToggle";

export default function Home() {
  const [messages, setMessages] = useState<string[]>([]);
  const [input, setInput] = useState("");
  const [jsonSchema, setJsonSchema] = useState("{}");
  const [history, setHistory] = useState<string[]>([]);

  const handleSend = () => {
    if (!input.trim()) return;

    setMessages((prev) => [...prev, `You: ${input}`]);
    setHistory((prev) => [...prev, `Пользователь: ${input}`]);

    setTimeout(() => {
      const reply = `AI: Обновляю схему по запросу "${input}"`;
      setMessages((prev) => [...prev, reply]);

      setJsonSchema((prev) => {
        try {
          const parsed = JSON.parse(prev);
          parsed.lastUpdated = input;
          return JSON.stringify(parsed, null, 2);
        } catch {
          return prev;
        }
      });
    }, 1000);

    setInput("");
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
            Инструмент для генерации и редактирования JSON-схем
          </p>
        </div>
        <ThemeToggle />
      </header>

      {/* Main content */}
      <main
        className="flex-1 flex overflow-hidden px-6 max-w-[1400px] w-full mx-auto gap-4 pb-4"
        style={{ minHeight: 0 }}
      >
        {/* Left half: Chat */}
        <div className="w-1/2 h-full flex flex-col min-h-0">
          <ChatWindow
            messages={messages}
            input={input}
            setInput={setInput}
            handleSend={handleSend}
          />
        </div>

        {/* Right half: Top 1/4 JSON, Bottom 1/4 History */}
        <div className="w-1/2 h-full flex flex-col min-h-0 gap-4">
          {/* JSON Editor (upper quarter) */}
          <div className="h-3/4 flex flex-col min-h-0">
            <JsonEditor json={jsonSchema} setJson={setJsonSchema} />
          </div>

          {/* History Panel (lower quarter) */}
          <div className="h-1/4 flex flex-col min-h-0">
            <HistoryPanel history={history} />
          </div>
        </div>
      </main>
    </div>
  );
}
