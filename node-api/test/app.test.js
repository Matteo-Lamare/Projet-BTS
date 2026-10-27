import { test } from 'node:test';
import assert from 'node:assert/strict';
import { createApp } from '../src/app.js';

test('health et API métier protégée', async () => {
  const server = createApp({ jwtSigningKey: 'x'.repeat(32), jwtIssuer: 'test', jwtAudience: 'test' }).listen(0);
  try {
    const base = `http://127.0.0.1:${server.address().port}`;
    const health = await fetch(base + '/health');
    assert.equal(health.status, 200);
    assert.deepEqual(await health.json(), { status: 'ok', runtime: 'node' });
    const protectedRoute = await fetch(base + '/api/students');
    assert.equal(protectedRoute.status, 401);
    const unknown = await fetch(base + '/unknown');
    assert.equal(unknown.status, 404);
  } finally {
    await new Promise((resolve, reject) => server.close(err => err ? reject(err) : resolve()));
  }
});
