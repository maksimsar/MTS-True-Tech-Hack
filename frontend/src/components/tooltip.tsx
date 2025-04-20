// src/components/tooltip.tsx
import { ReactNode } from "react";

export function Tooltip({ children }: { children: ReactNode }) {
  return <div className="relative group inline-block">{children}</div>;
}

export function TooltipTrigger({
  children,
  className = "",
}: {
  children: ReactNode;
  className?: string;
}) {
  return (
    <span
      className={`group-hover:underline cursor-help relative z-10 ${className}`}
    >
      {children}
    </span>
  );
}

export function TooltipContent({ children }: { children: ReactNode }) {
  return (
    <div
      className="
        absolute
        top-full                /* показываем сразу под триггером */
        left-1/2                /* по центру по горизонтали */
        -translate-x-1/2        /* сдвинуть влево на 50% */
        mt-2                    /* отступ сверху */
        px-2 py-1 text-xs
        rounded shadow
        opacity-0
        group-hover:opacity-100 transition-opacity
        z-20
      "
      style={{
        backgroundColor: "var(--surface)",
        color: "var(--fg)",
      }}
    >
      {children}
    </div>
  );
}
