/**
 * @jest-environment jsdom
 */
import *  as core from '@dative-gpi/foundation-extension-core-ui';
import *  as admin from '@dative-gpi/foundation-extension-admin-ui';

test('basic', () => {
  // expect(core).toBeDefined();
  expect(admin).toBeDefined();
});