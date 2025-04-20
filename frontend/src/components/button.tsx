import { ButtonHTMLAttributes, ReactNode } from "react";

export function Button({
  children,
  className = "",
  style,
  ...props
}: ButtonHTMLAttributes<HTMLButtonElement> & { children: ReactNode }) {
  return (
    <button
      {...props}
      className={`rounded-full h-10 w-10 font-medium ${className}`}
      style={{
        backgroundColor: 'var(--accent)',
        color: '#fff',
        ...style,
      }}
    >
      {children}
    </button>
  );
}
