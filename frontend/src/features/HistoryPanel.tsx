import { Card, CardContent } from "../components/card"
import { ScrollArea } from "../components/scroll-area"


type Props = {
  history: string[];
};

export default function HistoryPanel({ history }: Props) {
  return (
    <Card className="col-span-1 md:col-span-1 flex flex-col">
      <CardContent className="flex-1 p-4">
        <h2 className="text-lg font-semibold mb-2">История изменений</h2>
        <ScrollArea className="space-y-2">
          {history.map((entry, idx) => (
            <div key={idx} className="text-xs bg-white text-zinc-900 p-2 rounded-md dark:bg-zinc-700 dark:text-white">
              {entry}
            </div>
          ))}
        </ScrollArea>
      </CardContent>
    </Card>
  );
}
