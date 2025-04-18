import { ReactNode } from "react";

export function Tooltip({ children }: { children: ReactNode }) {
  return <div className="relative group inline-block">{children}</div>;
}

export function TooltipTrigger({ children, className = "" }: { children: ReactNode; className?: string }) {
    return <span className={`group-hover:underline cursor-help relative z-10 ${className}`}>{children}</span>;
  }
export function TooltipContent({ children }: { children: ReactNode }) {
  return (
    <div className="absolute bottom-full left-1/2 -translate-x-1/2 mb-2 px-2 py-1 bg-zinc-700 text-white text-xs rounded shadow opacity-0 group-hover:opacity-100 transition-opacity">
      {children}
    </div>
  );
}
