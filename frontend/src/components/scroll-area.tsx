import { ReactNode } from "react";

export function ScrollArea({ children, className = "" }: { children: ReactNode; className?: string }) {
  return (
    <div className={`overflow-y-auto h-full pr-2 ${className}`} style={{ color: 'var(--fg)' }}>
      {children}
    </div>
  );
}
