import { useState } from "react";
import { Card, CardContent } from "./components/card";
import { Input } from "./components/input";
import { Button } from "./components/button";
import { Textarea } from "./components/textarea";
import { ScrollArea } from "./components/scroll-area";
import { Tooltip, TooltipTrigger, TooltipContent } from "./components/tooltip";

function App() {
  const [messages, setMessages] = useState<string[]>([]);
  const [input, setInput] = useState("");
  const [jsonSchema, setJsonSchema] = useState("{}");
  const [history, setHistory] = useState<string[]>([]);

  const handleSend = () => {
    if (!input.trim()) return;

    const newMessages = [...messages, `You: ${input}`];
    setMessages(newMessages);
    setHistory((prev) => [...prev, `User edited: ${input}`]);

    // Fake LLM reply (replace with real backend call)
    setTimeout(() => {
      const fakeReply = `AI: Понял запрос — обновляю JSON...`;
      setMessages((msgs) => [...msgs, fakeReply]);

      // Fake JSON update
      setJsonSchema((prev) => {
        try {
          const parsed = JSON.parse(prev);
          parsed.lastUpdatedBy = input;
          return JSON.stringify(parsed, null, 2);
        } catch {
          return prev;
        }
      });
    }, 1000);

    setInput("");
  };

  return (
    <div className="dark min-h-screen bg-zinc-900 text-white p-4 grid grid-cols-1 md:grid-cols-3 gap-4">
      {/* Chat Area */}
      <Card className="col-span-1 md:col-span-1 flex flex-col">
        <CardContent className="flex-1 flex flex-col p-4">
          <ScrollArea className="flex-1 space-y-2">
            {messages.map((msg, idx) => (
              <div key={idx} className="text-sm bg-zinc-800 p-2 rounded-md">
                {msg}
              </div>
            ))}
          </ScrollArea>
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

      {/* JSON Editor */}
      <Card className="col-span-1 md:col-span-1 flex flex-col">
        <CardContent className="flex-1 p-4">
          <div className="flex items-center justify-between mb-2">
            <h2 className="text-lg font-semibold">JSON-схема</h2>
            <Tooltip>
              <TooltipTrigger className="text-zinc-400 text-sm cursor-help">?</TooltipTrigger>
              <TooltipContent>
                Вы можете напрямую редактировать JSON здесь.
              </TooltipContent>
            </Tooltip>
          </div>
          <Textarea
            className="w-full h-full bg-zinc-800 text-white font-mono text-sm"
            value={jsonSchema}
            onChange={(e) => setJsonSchema(e.target.value)}
          />
        </CardContent>
      </Card>

      {/* History Panel */}
      <Card className="col-span-1 md:col-span-1 flex flex-col">
        <CardContent className="flex-1 p-4">
          <h2 className="text-lg font-semibold mb-2">История изменений</h2>
          <ScrollArea className="space-y-2">
            {history.map((entry, idx) => (
              <div key={idx} className="text-xs bg-zinc-800 p-2 rounded-md">
                {entry}
              </div>
            ))}
          </ScrollArea>
        </CardContent>
      </Card>
    </div>
  );
}

export default App;
