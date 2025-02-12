const { test, expect } = require('@playwright/test');

test('Browser Context Playwright Test', async ({browser}) => {
    const context = await browser.newContext();
    const page = await context.newPage();
    await page.goto("https://www.google.com/maps");

    console.log(await page.title());
    await expect(page).toHaveTitle("Google Maps");
});

test('Page Playwright Test', async ({page}) => {
    await page.goto("https://www.google.com/");
    console.log(await page.title());
});

test('Test Selectors', async ({ page }) => {
    await page.goto("https://rahulshettyacademy.com/loginpagePractise/");
    console.log(await page.title());

    await page.locator("#username").fill("rahulshettyacademy");
    await page.locator("#password").fill("learning");

    await page.locator("#signInBtn").click();

    await page.pause();
});