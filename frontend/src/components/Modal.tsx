import { ReactNode } from "react";

type Props = {
  isOpen: boolean;
  onClose: () => void;
  children: ReactNode;
};

export default function Modal({ isOpen, onClose, children }: Props) {
  if (!isOpen) return null;

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center bg-black/50 backdrop-blur-sm transition-opacity duration-300">
      <div className="bg-[var(--surface)] text-[var(--fg)] rounded-xl shadow-2xl w-[90vw] h-[85vh] p-6 flex flex-col relative animate-fade-in-up">
        {/* Кнопка закрытия */}
        <button
          onClick={onClose}
          className="absolute top-4 right-4 text-xl text-zinc-400 hover:text-red-500 transition"
          title="Закрыть"
        >
          ✕
        </button>

        {/* Контент (растягиваем вниз) */}
        <div className="flex-1 overflow-hidden mt-2 flex flex-col p-3"> {/* Увеличиваем padding до p-3 */}
          {children}
        </div>
      </div>
    </div>
  );
}