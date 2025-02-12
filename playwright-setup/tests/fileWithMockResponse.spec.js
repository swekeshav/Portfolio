const { test, expect } = require('@playwright/test');

// Mock API response data
const mockUserData = {
  id: 1,
  name: "John Doe",
  email: "john.doe@example.com",
};

test.only('should display user data with mocked API response', async ({ page }) => {
  // Intercept the API request and serve mock response
  await page.route('**/api/user', async (route) => {
    await route.fulfill({
      status: 200,
      contentType: 'application/json',
      body: JSON.stringify(mockUserData),
    });
  });

  // Navigate to the page that makes the API call
  await page.goto('http://localhost:3000/dashboard');

  // Assertions to check if mocked data is displayed
  await expect(page.locator('#user-name')).toHaveText('John Doe');
  await expect(page.locator('#user-email')).toHaveText('john.doe@example.com');
});