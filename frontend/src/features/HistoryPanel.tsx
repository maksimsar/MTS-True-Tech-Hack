import { useEffect, useRef } from "react";
import { Card, CardContent } from "../components/card";
import { ScrollArea } from "../components/scroll-area";

type Props = {
  history: string[];
};

export default function HistoryPanel({ history }: Props) {
  const scrollRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    const el = scrollRef.current;
    if (el) {
      el.scrollTop = el.scrollHeight;
    }
  }, [history]);

  return (
    <Card className="col-span-1 md:col-span-1 flex flex-col h-full min-h-0">
      <CardContent className="flex flex-col flex-1 p-4 min-h-0">
        <h2 className="flex-none text-lg font-semibold mb-2">
          История изменений
        </h2>

        <div
          ref={scrollRef}
          className="flex-1 min-h-0 space-y-2 overflow-y-auto custom-scroll"
        >
          {history.map((entry, idx) => (
            <div
              key={idx}
              className="text-xs bg-white text-zinc-900 dark:bg-zinc-700 dark:text-white
                p-2 rounded-md opacity-0 animate-fade-in-up"
              style={{ animationDelay: `${idx * 40}ms` }}
            >
              {entry}
            </div>
          ))}
        </div>
      </CardContent>
    </Card>
  );
}
