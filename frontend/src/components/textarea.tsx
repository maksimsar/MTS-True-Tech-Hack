import { TextareaHTMLAttributes } from "react";

export function Textarea(props: TextareaHTMLAttributes<HTMLTextAreaElement>) {
  return (
    <textarea
      {...props}
      className={`
        rounded px-3 py-2 w-full resize-none font-mono transition outline-none border border-gray-400
        focus:ring-2 ring-[var(--accent)] bg-transparent
        overflow-y-auto custom-scroll
      `}
      
      style={{
        backgroundColor: "var(--surface)",
        color: "var(--fg)",
      }}
    />
  );
}
