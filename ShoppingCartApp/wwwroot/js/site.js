// Find the theme value in our browser's local storage.
let theme = localStorage.getItem('Theme');
// If the value is set for secondary, load the secondary theme css file, else load the
// primary theme css file.
if (theme === "Secondary")
{
    document.getElementById('themeStyle').setAttribute('href', '/css/secondary-theme.css');
}
else
{
    document.getElementById('themeStyle').setAttribute('href', '/css/primary-theme.css');
}

// Add a listener to the browser window that runs once the page finishes loading. 
window.addEventListener('load', () =>
{
    // Find the button with the btnTheme ID on it and add a listener that makes it
    // run the switchTheme() method when clicked. 
    document.getElementById('btnTheme').addEventListener('click', () => switchTheme())
})

function switchTheme()
{
    // Look at the current theme that is selected in the settings.
    let currentTheme = localStorage.getItem('Theme');
    // Look at which theme mode is currently set and swap to opposite.
    if (currentTheme === "Secondary")
    {
        // Change the CSS file to the opposite of this if branch.
        document.getElementById('themeStyle').setAttribute('href', '/css/primary-theme.css');
        // Set the new theme in the local storage.
        localStorage.setItem('Theme', 'Primary');
    }
    else
    {
        // Change the CSS file to the opposite of this if branch.
        document.getElementById('themeStyle').setAttribute('href', '/css/secondary-theme.css');
        // Set the new theme in the local storage.
        localStorage.setItem('Theme', 'Secondary');
    }
}