// src/features/JsonEditor.tsx
import { Card, CardContent } from "../components/card";
import { Tooltip, TooltipTrigger, TooltipContent } from "../components/tooltip";
import { Textarea } from "../components/textarea";

type Props = {
  json: string;
  setJson: (val: string) => void;
};

export default function JsonEditor({ json, setJson }: Props) {
  return (
    <Card className="col-span-1 md:col-span-1 flex flex-col h-full min-h-0">
      <CardContent className="flex flex-col flex-1 p-4 min-h-0">
        {/* Заголовок */}
        <div className="flex-none flex items-center justify-between">
          <h2 className="text-lg font-semibold">JSON‑схема</h2>
          <Tooltip>
            <TooltipTrigger className="text-sm text-[var(--fg)]">?</TooltipTrigger>
            <TooltipContent>
              Вы можете напрямую редактировать JSON здесь.
            </TooltipContent>
          </Tooltip>
        </div>

        {/* Растягивающийся Textarea, он сам скроллится */}
        <div className="flex-1 min-h-0 mt-2">
          <Textarea
            value={json}
            onChange={(e) => setJson(e.target.value)}
            className="h-full"
          />
        </div>
      </CardContent>
    </Card>
  );
}
