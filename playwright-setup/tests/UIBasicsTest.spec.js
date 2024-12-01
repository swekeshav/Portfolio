const { test } = require('@playwright/test');

test('Browser Context Playwright Test', async ({browser}) => {
    const context = await browser.newContext();
    const page = await context.newPage();
    await page.goto("https://www.google.com/");
});

test('Page Playwright Test', async ({page}) => {
    await page.goto("https://www.google.com/");
});