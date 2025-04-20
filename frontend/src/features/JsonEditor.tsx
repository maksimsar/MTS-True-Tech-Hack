import { useState } from "react";
import { Card, CardContent } from "../components/card";
import { Tooltip, TooltipTrigger, TooltipContent } from "../components/tooltip";
import { Textarea } from "../components/textarea";
import { Copy, Check, Expand } from "lucide-react";
import Modal from "../components/Modal";

type Props = {
  json: string;
  setJson: (val: string) => void;
};

export default function JsonEditor({ json, setJson }: Props) {
  const [copied, setCopied] = useState(false);
  const [fullscreen, setFullscreen] = useState(false);

  const handleCopy = async () => {
    try {
      await navigator.clipboard.writeText(json);
      setCopied(true);
      setTimeout(() => setCopied(false), 2000);
    } catch {
      // ignore
    }
  };

  return (
    <>
      <Card className="flex flex-col h-full min-h-0">
        <CardContent className="grid grid-rows-[auto_1fr] h-full p-4 min-h-0">
          <div className="flex items-center justify-between mb-2">
            <h2 className="text-lg font-semibold">JSON‑схема</h2>
            <div className="flex items-center gap-2">
              <Tooltip>
                <TooltipTrigger className="text-sm text-[var(--fg)]">?</TooltipTrigger>
                <TooltipContent>
                  Вы можете напрямую редактировать JSON здесь.
                </TooltipContent>
              </Tooltip>

              <button
                onClick={handleCopy}
                className="p-1 rounded-md hover:bg-gray-100 dark:hover:bg-gray-700 transition-transform active:scale-90"
                title="Скопировать JSON"
              >
                {copied ? (
                  <Check
                    size={20}
                    className="text-green-500 transform scale-110 transition-transform duration-200"
                  />
                ) : (
                  <Copy size={20} className="text-gray-500 dark:text-gray-400" />
                )}
              </button>

              <button
                onClick={() => setFullscreen(true)}
                className="p-1 rounded-md hover:bg-gray-100 dark:hover:bg-gray-700 transition-transform active:scale-90"
                title="Открыть во весь экран"
              >
                <Expand size={20} className="text-gray-500 dark:text-gray-400" />
              </button>
            </div>
          </div>

          <Textarea
            value={json}
            onChange={(e) => setJson(e.target.value)}
            className="w-full h-full resize-none overflow-auto"
            style={{ minHeight: 0 }}
          />
        </CardContent>
      </Card>

      {/* Modal Viewer */}
      <Modal isOpen={fullscreen} onClose={() => setFullscreen(false)}>
        <h2 className="text-xl font-semibold mb-4">Полноэкранный просмотр</h2>
        <Textarea
          value={json}
          onChange={(e) => setJson(e.target.value)}
          className="h-[calc(100%-2rem)]"
        />
      </Modal>
    </>
  );
}