import { render, screen } from '@testing-library/react';
import App from './App.tsx';

test('renders the form, the grid, and the submit button', () => {
  const { container } = render(<App />);
  expect(container.querySelector('.dx-form')).toBeTruthy();
  expect(container.querySelector('.dx-datagrid')).toBeTruthy();
  expect(screen.getByText('Validate and Submit')).toBeTruthy();
});
