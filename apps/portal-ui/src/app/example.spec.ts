// Simple example test
describe('Math operations', () => {
  test('should add two numbers correctly', () => {
    expect(2 + 2).toBe(4);
  });

  test('should multiply two numbers correctly', () => {
    expect(3 * 4).toBe(12);
  });
});

// Example test for a service or utility function
describe('String operations', () => {
  test('should convert string to uppercase', () => {
    const input = 'hello world';
    const result = input.toUpperCase();
    expect(result).toBe('HELLO WORLD');
  });
});
