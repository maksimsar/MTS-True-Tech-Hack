import { Card, CardContent } from "../components/card";
import { ScrollArea } from "../components/scroll-area";
import { Tooltip, TooltipTrigger, TooltipContent } from "../components/tooltip";
import { Textarea } from "../components/textarea"; 

type Props = {
  json: string;
  setJson: (val: string) => void;
};
export default function JsonEditor({ json, setJson }: Props) {
  return (
  <Card className="col-span-1 md:col-span-1 flex flex-col">
    <CardContent className="p-4 space-y-2">
      <div className="flex items-center justify-between">
        <h2 className="text-lg font-semibold">JSON-схема</h2>
        <Tooltip>
          <TooltipTrigger className="text-sm text-[var(--fg)]">?</TooltipTrigger>
          <TooltipContent>Вы можете напрямую редактировать JSON здесь.</TooltipContent>
        </Tooltip>
      </div>
      <Textarea
        value={json}
        onChange={(e) => setJson(e.target.value)}
        className="min-h-[200px]"
      />
    </CardContent>
  </Card>


  );
}
