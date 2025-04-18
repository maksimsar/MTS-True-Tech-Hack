import { useState } from "react";
import ChatWindow from "../features/ChatWindow";
import JsonEditor from "../features/JsonEditor";
import HistoryPanel from "../features/HistoryPanel";
import ThemeToggle from '../components/ThemeToggle';


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
        <div className="min-h-screen px-6 py-10"
    style={{
        backgroundColor: 'var(--bg)',
        color: 'var(--fg)',
    }}
    >
    <header className="flex justify-between items-center mb-8 max-w-[1400px] mx-auto">
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

    <main className="grid grid-cols-1 md:grid-cols-3 gap-6 max-w-[1400px] mx-auto">
        <ChatWindow
        messages={messages}
        input={input}
        setInput={setInput}
        handleSend={handleSend}
        />
        <JsonEditor json={jsonSchema} setJson={setJsonSchema} />
        <HistoryPanel history={history} />
    </main>
    </div>

  );
}
