module.exports = {
    content: [
        './**/*.cshtml',
        './wwwroot/**/*.js'
    ],
    theme: {
        extend: {},
    },
    plugins: [
        require('@tailwindcss/aspect-ratio'),
    ],
};
