import { ButtonHTMLAttributes, ReactNode } from "react";

export function Button({
  children,
  ...props
}: ButtonHTMLAttributes<HTMLButtonElement> & { children: ReactNode }) {
  return (
    <button
      {...props}
      className="rounded-lg px-4 py-2 font-medium transition"
      style={{
        backgroundColor: 'var(--accent)',
        color: '#fff',
      }}
    >
      {children}
    </button>
  );
}
