import { ReactNode } from "react";

export function Tooltip({ children }: { children: ReactNode }) {
  return (
    <div className="relative group inline-block">
      {children}
    </div>
  );
}

export function TooltipTrigger({
  children,
  className = "",
}: {
  children: ReactNode;
  className?: string;
}) {
  return (
    <span className={`cursor-help text-sm ${className}`}>
      {children}
    </span>
  );
}

export function TooltipContent({ children }: { children: ReactNode }) {
  return (
    <div
      className="
        absolute z-20
        left-1/2 -translate-x-1/2 top-full mt-2
        w-max max-w-[200px]
        px-3 py-1 rounded-md text-xs
        opacity-0 scale-95
        group-hover:opacity-100 group-hover:scale-100
        transition-all duration-200 ease-out
        pointer-events-none
        shadow-md
        bg-[var(--surface)] text-[var(--fg)]
      "
    >
      {children}
    </div>
  );
}
