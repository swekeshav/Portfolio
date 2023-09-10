// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function tabLoadEventHandler() {
    let hash = 'tab_' + +new Date();
    sessionStorage.setItem('TabHash', hash);
    let tabs = JSON.parse(localStorage.getItem('TabsOpen') || '{ }');
    tabs[hash] = true;
    localStorage.setItem('TabsOpen', JSON.stringify(tabs));
}
function tabUnloadEventHandler() {
    let hash = sessionStorage.getItem('TabHash');
    let tabs = JSON.parse(localStorage.getItem('TabsOpen') || '{ }');
    delete tabs[hash];
    localStorage.setItem('TabsOpen', JSON.stringify(tabs));

    let tabsCount = Object.keys(JSON.parse(localStorage.getItem('TabsOpen') || '{}')).length
}
