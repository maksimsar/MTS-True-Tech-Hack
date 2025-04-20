// src/components/scroll-area.tsx
import { forwardRef, ReactNode } from "react";

export const ScrollArea = forwardRef<
  HTMLDivElement,
  { children: ReactNode; className?: string }
>(({ children, className = "" }, ref) => (
  <div
    ref={ref}
    className={`overflow-y-auto h-full pr-2 ${className}`}
    style={{ color: "var(--fg)" }}
  >
    {children}
  </div>
));
ScrollArea.displayName = "ScrollArea";
