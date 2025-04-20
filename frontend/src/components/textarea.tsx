// src/components/textarea.tsx
import { TextareaHTMLAttributes } from "react";

export function Textarea(props: TextareaHTMLAttributes<HTMLTextAreaElement>) {
  return (
    <textarea
      {...props}
      className={`
        rounded px-3 py-2 w-full h-full resize-none font-mono
        outline-none border border-gray-400
        focus:ring-2 focus:ring-[var(--accent)] focus:ring-opacity-50
        transition-all duration-300
        text-sm leading-relaxed
        overflow-auto custom-scroll whitespace-pre-wrap
      `}
      style={{
        backgroundColor: "var(--surface)",
        color: "var(--fg)",
      }}
    />
  );
}