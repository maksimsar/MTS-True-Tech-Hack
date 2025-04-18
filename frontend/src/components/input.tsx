import { InputHTMLAttributes } from "react";

export function Input(props: InputHTMLAttributes<HTMLInputElement>) {
  return <input {...props} className="bg-zinc-700 text-white rounded px-3 py-2 w-full outline-none focus:ring-2 ring-blue-500" />;
}