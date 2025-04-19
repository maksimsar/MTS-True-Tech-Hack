import { useRef, useEffect } from "react";
import { Input } from "../components/input";
import { Button } from "../components/button";
import { Card, CardContent } from "../components/card";
import { ScrollArea } from "../components/scroll-area";

type Props = {
  messages: string[];
  input: string;
  setInput: (val: string) => void;
  handleSend: () => void;
};

export default function ChatWindow({ messages, input, setInput, handleSend }: Props) {
  const scrollRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    const el = scrollRef.current;
    if (el) el.scrollTop = el.scrollHeight;
  }, [messages]);

  return (
    <Card className="col-span-1 md:col-span-1 flex flex-col h-full">
      <CardContent className="flex-1 flex flex-col p-4 overflow-hidden">
        <div ref={scrollRef} className="flex-1 overflow-y-auto space-y-2 pr-2">
          {messages.map((msg, idx) => (
            <div
              key={idx}
              className="text-sm p-2 rounded-md bg-white text-zinc-900 dark:bg-zinc-700 dark:text-white"
            >
              {msg}
            </div>
          ))}
        </div>
        <div className="flex gap-2 mt-4">
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