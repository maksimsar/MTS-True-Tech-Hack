import { InputHTMLAttributes } from "react";

export function Input(props: InputHTMLAttributes<HTMLInputElement>) {
  return (
    <input
      {...props}
      className="rounded px-3 py-2 w-full outline-none border border-gray-400 focus:ring-2 ring-[var(--accent)] bg-transparent transition"
      style={{ color: 'var(--fg)', backgroundColor: 'var(--surface)' }}
    />
  );
}
