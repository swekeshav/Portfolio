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
    const userName = page.locator("#username");
    const signInBtn = page.locator("#signInBtn");

    await page.goto("https://rahulshettyacademy.com/loginpagePractise/");
    console.log(await page.title());

    //1st Attempt
    await userName.fill("rahulshetty");
    await page.locator("#password").fill("learning");

    await signInBtn.click();

    // var content = await page.locator("[style *= 'block']").textContent();
    // console.log(content);

    await expect(page.locator("[style *= 'block']")).toContainText("Incorrect username/password.")

    //2nd Attempt
    await userName.fill("");
    await userName.fill("rahulshettyacademy");
    
    await signInBtn.click();

    console.log(await page.locator('.card-body .card-title').nth(0).textContent());
    console.log(await page.locator('.card-body .card-title').nth(1).textContent());
});