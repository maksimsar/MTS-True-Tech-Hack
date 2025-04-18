import colors from 'tailwindcss/colors'

export default {
  darkMode: 'class', // ⬅️ обязательно
  content: ['./index.html', './src/**/*.{js,ts,jsx,tsx}'],
  theme: {
    extend: {
      colors: {
        brand: {
          DEFAULT: '#EF3124',
          dark: '#C62820',
          light: '#FF6655',
        },
        gray: colors.zinc,
        surface: {
          light: '#F4F4F4',
          dark: '#1C1C1C',
        },
        text: {
          light: '#111111',
          dark: '#F1F1F1',
        },
      },
      fontFamily: {
        sans: ['Inter', 'system-ui', 'sans-serif'],
      },
    },
  },
  plugins: [],
}
