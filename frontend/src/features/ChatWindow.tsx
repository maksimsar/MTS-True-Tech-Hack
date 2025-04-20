// src/features/ChatWindow.tsx
import { useRef, useEffect } from "react";
import { Input } from "../components/input";
import { Button } from "../components/button";
import { Card, CardContent } from "../components/card";

type Props = {
  messages: string[];
  input: string;
  setInput: (val: string) => void;
  handleSend: () => void;
};

export default function ChatWindow({
  messages,
  input,
  setInput,
  handleSend,
}: Props) {
  const scrollRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    const el = scrollRef.current;
    if (el) el.scrollTop = el.scrollHeight;
  }, [messages]);

  return (
    <Card className="col-span-1 md:col-span-1 flex flex-col h-full min-h-0 rounded-xl shadow-sm transition">
      <CardContent className="flex flex-col flex-1 p-4 min-h-0">
        {/* Скролл-блок */}
        <div
          ref={scrollRef}
          className="flex-1 min-h-0 overflow-y-auto space-y-2 pr-2 custom-scroll"
        >
        {messages.map((msg, idx) => (
          <div
            key={idx}
            className="
              text-sm p-2 rounded-md bg-white text-zinc-900 dark:bg-zinc-700 dark:text-white
              opacity-0 animate-fade-in-up
            "
            style={{ animationDelay: `${idx * 40}ms` }} // волна сообщений
          >
            {msg}
          </div>
        ))}

        </div>

        {/* Поле ввода */}
 <div className="flex-none flex gap-2 mt-4 items-end bg-white dark:bg-zinc-800 border-none dark:border-zinc-700 rounded-2xl px-2 py-2 shadow-sm">
  <Input
    placeholder="Опишите бизнес-процесс..."
    value={input}
    onChange={(e) => setInput(e.target.value)}
    onKeyDown={(e) => e.key === "Enter" && handleSend()}
    className="flex-1"
  />
  <div>
    <Button
      onClick={handleSend}
      className="bg-red-500 hover:bg-red-700 text-white rounded-full h-10 w-10 flex items-center justify-center shrink-0"
    >
      ↑
    </Button>
  </div>
</div>
      </CardContent>
    </Card>
  );
}
