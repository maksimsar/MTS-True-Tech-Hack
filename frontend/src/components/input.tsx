// src/components/input.tsx
import { TextareaHTMLAttributes, useRef, useLayoutEffect } from "react";

export function Input(props: TextareaHTMLAttributes<HTMLTextAreaElement>) {
  const ref = useRef<HTMLTextAreaElement>(null);
  const MAX_ROWS = 10;

  useLayoutEffect(() => {
    const el = ref.current;
    if (!el) return;

    el.style.height = "auto";

    const computed = getComputedStyle(el);
    const lineHeight = parseFloat(computed.lineHeight);
    const maxHeight = lineHeight * MAX_ROWS;

    const newHeight = Math.min(el.scrollHeight, maxHeight);
    el.style.height = `${newHeight}px`;

    el.style.overflowY = el.scrollHeight > newHeight ? "auto" : "hidden";
  }, [props.value]);

  return (
    <textarea
      {...props}
      ref={ref}
      rows={1}
      className={`
        rounded px-3 py-2 w-full
        outline-none border-none
        focus:ring-0
        bg-transparent transition
        resize-none pr-1
        custom-scroll
        text-sm text-zinc-900 dark:text-white
        placeholder-gray-400 dark:placeholder-zinc-500
      `}
      style={{
        maxHeight: `${MAX_ROWS}em`,
        backgroundColor: "transparent",
      }}
    />
  );
}

