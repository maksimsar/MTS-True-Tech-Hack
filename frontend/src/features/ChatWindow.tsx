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
          {messages.map((msg, idx) => {
            const isUser = msg.startsWith("You:");
            return (
              <div
                key={idx}
                className={`${
                  isUser
                    ? "self-end bg-blue-100 text-right"
                    : "self-start bg-gray-100 text-left"
                } text-sm p-2 rounded-md dark:bg-zinc-700 dark:text-white opacity-0 animate-fade-in-up`}
                style={{ animationDelay: `${idx * 40}ms` }} // волна сообщений
              >
                {msg}
              </div>
            );
          })}
        </div>

        {/* Поле ввода */}
        <div className="flex-none flex gap-2 mt-4">
          <Input
            className="flex-1"
            placeholder="Введите описание или команду..."
            value={input}
            onChange={(e) => setInput(e.target.value)}
            onKeyDown={(e) => e.key === "Enter" && handleSend()}
          />
          <Button onClick={handleSend}>Отправить</Button>
        </div>
      </CardContent>
    </Card>
  );
}
