import { ReactNode } from 'react'

export function Card({ children, className = "" }: { children: ReactNode; className?: string }) {
  return (
    <div
      className={`rounded-2xl p-4 shadow-md ${className}`}
      style={{
        backgroundColor: 'var(--surface)',
        color: 'var(--fg)',
      }}
    >
      {children}
    </div>
  )
}

export function CardContent({ children, className = "" }: { children: ReactNode; className?: string }) {
  return <div className={`space-y-2 ${className}`}>{children}</div>
}
