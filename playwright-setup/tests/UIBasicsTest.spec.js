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

test('Reading TextContent', async ({ page }) => {
    const userName = page.locator("#username");
    const signInBtn = page.locator("#signInBtn");

    await page.goto("https://rahulshettyacademy.com/loginpagePractise/");
    console.log(await page.title());

    await userName.fill("rahulshetty");
    await page.locator("#password").fill("learning");

    await signInBtn.click();

    await expect(page.locator("[style *= 'block']")).toContainText("Incorrect username/password.")
});

test.only('Iterating through Selectors', async ({ page }) => {
    const userName = page.locator("#username");
    const signInBtn = page.locator("#signInBtn");
    const cardTitles = page.locator('.card-body .card-title');

    await page.goto("https://rahulshettyacademy.com/loginpagePractise/");
    console.log(await page.title());

    await userName.fill("rahulshettyacademy");
    await page.locator("#password").fill("learning");
    
    await signInBtn.click();

    
    // This will return 0 elements because allTextContents() OR all() does not wait for elements to match the locator,
    // and instead immediately returns whatever is present on the page
    
    // var productTitles = await cardTitles.all();
    // console.log(productTitles);
    // productTitles.forEach(async el => {
    //     console.log(await el.textContent());
    // });
    
    // var productTitles = await cardTitles.allTextContents();
    // console.log(productTitles); 
    
    // This works because textContent() waits for elements to match the locator
    console.log(await page.locator('.card-body .card-title').nth(0).textContent());
    console.log(await page.locator('.card-body .card-title').nth(1).textContent());
});