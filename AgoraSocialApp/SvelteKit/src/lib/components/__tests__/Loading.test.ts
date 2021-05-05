import { render } from '@testing-library/svelte';
import Loading from '../Loading.svelte';

describe('Loading Component', () => {
  it('should render', () => {
    const { container } = render(Loading);

    expect(container).toMatchSnapshot();
  });
  it('should show the loading indicator', () => {
    const { container } = render(Loading);
    expect(container).toHaveTextContent('♦');
  });
});
