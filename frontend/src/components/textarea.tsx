import { TextareaHTMLAttributes } from "react";

export function Textarea(props: TextareaHTMLAttributes<HTMLTextAreaElement>) {
  return <textarea {...props} className="bg-zinc-700 text-white rounded px-3 py-2 w-full h-full outline-none resize-none font-mono focus:ring-2 ring-blue-500" />;
}