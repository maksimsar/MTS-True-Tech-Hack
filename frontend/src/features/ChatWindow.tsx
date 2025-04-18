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
  return (
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
  );
}
