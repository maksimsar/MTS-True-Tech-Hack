import { useTheme } from "../hooks/useTheme"
import { Sun, Moon } from 'lucide-react'

export default function ThemeToggle() {
  const { theme, toggleTheme } = useTheme()

  return (
    <button
      onClick={toggleTheme}
      className="flex items-center gap-2 px-3 py-1 border border-gray-400 rounded-full text-sm hover:border-brand transition"
    >
      {theme === 'dark' ? <Sun size={16} /> : <Moon size={16} />}
      {theme === 'dark' ? 'Светлая' : 'Тёмная'} тема
    </button>
  )
}
