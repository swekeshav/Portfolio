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

test.only('Test Selectors', async ({ page }) => {
    await page.goto("https://rahulshettyacademy.com/loginpagePractise/");
    console.log(await page.title());

    await page.locator("#username").fill("rahulshetty");
    await page.locator("#password").fill("learning");

    await page.locator("#signInBtn").click();

    // var content = await page.locator("[style *= 'block']").textContent();
    // console.log(content);

    await expect(page.locator("[style *= 'block']")).toContainText("Incorrect username/password.")
});