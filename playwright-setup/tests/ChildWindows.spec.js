const { test, expect } = require('@playwright/test');

test.only('Handle child windows', async ({ browser }) => {
    const context = await browser.newContext();
    const page = await context.newPage();

    const userId = 'rahulshettyacademy';

    await page.goto("https://rahulshettyacademy.com/loginpagePractise");
    const documentLink = page.locator("[href*='documents-request']");
    
    const [newPage] = await Promise.all(
        [
            context.waitForEvent('page'),
            documentLink.click()
        ]);
    
    const text = await newPage.locator('.red a').textContent();
    const domainName = text.split('@')[1].split(' ')[0];

    console.log(userId + '@' + domainName);
});