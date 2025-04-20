// src/components/input.tsx
import { TextareaHTMLAttributes, useRef, useLayoutEffect } from "react";

export function Input(props: TextareaHTMLAttributes<HTMLTextAreaElement>) {
  const ref = useRef<HTMLTextAreaElement>(null);
  const MAX_ROWS = 10;

  useLayoutEffect(() => {
    const el = ref.current;
    if (!el) return;

    // Сброс высоты, чтобы считать scrollHeight
    el.style.height = "auto";
    const computed = getComputedStyle(el);
    const lineHeight = parseFloat(computed.lineHeight);

    // Максимальная высота в пикселях = высота строки * MAX_ROWS
    const maxHeight = lineHeight * MAX_ROWS;
    const newHeight = Math.min(el.scrollHeight, maxHeight);

    el.style.height = `${newHeight}px`;
  }, [props.value]);

  return (
    <textarea
      {...props}
      ref={ref}
      rows={1}
      className="
        rounded px-3 py-2 w-full
        outline-none border border-gray-400
        focus:ring-2 ring-[var(--accent)]
        bg-transparent transition
        resize-none overflow-y-auto
      "
      style={{
        color: "var(--fg)",
        backgroundColor: "var(--surface)",
        // на всякий случай дублируем maxHeight в CSS
        maxHeight: `${MAX_ROWS}em`,
      }}
    />
  );
}
