import { NgCliExamplePage } from './app.po';

describe('ng-cli-example App', function() {
  let page: NgCliExamplePage;

  beforeEach(() => {
    page = new NgCliExamplePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
