import { ReactNode } from "react";

export function ScrollArea({ children, className = "" }: { children: ReactNode; className?: string }) {
  return <div className={`overflow-y-auto h-full pr-2 ${className}`}>{children}</div>;
}