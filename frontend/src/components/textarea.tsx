import { TextareaHTMLAttributes } from "react";

export function Textarea(props: TextareaHTMLAttributes<HTMLTextAreaElement>) {
  return (
    <textarea
      {...props}
      className={`rounded-xl px-3 py-2 w-full resize-none font-mono transition outline-none border border-gray-500 focus:ring-2 ring-[var(--accent)] bg-transparent`}
      style={{
        backgroundColor: 'var(--surface)',
        color: 'var(--fg)',
      }}
    />
  );
}

