import { ButtonHTMLAttributes, ReactNode } from "react";

export function Button({ children, ...props }: ButtonHTMLAttributes<HTMLButtonElement> & { children: ReactNode }) {
  return <button {...props} className="bg-blue-600 hover:bg-blue-500 text-white px-4 py-2 rounded shadow" >{children}</button>;
}
