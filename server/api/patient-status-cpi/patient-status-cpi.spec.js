'use strict';

var should = require('should');
var app = require('../../app');
var request = require('supertest');

var token = null;

before(function (done) {
  request(app)
    .post('/api/auth/local')
    .send({ username: "admin", password: "admin"})
    .expect(200)
    .expect('Content-Type', /json/)
    .end(function (err, res) {
      token = res.body.token;
      if (token) {
        token = '/?access_token=' + token;
      }
      done(null);
    })
})

describe('GET /api/patient/:patientId/cpi-status', function() {
  var patientId = 1;

  it('should respond with JSON array', function(done) {
    request(app)
      .get('/api/patient/' + patientId + '/cpi-status' + token)
      .expect(200)
      .expect('Content-Type', /json/)
      .end(function(err, res) {
        if (err) return done(err);
        res.body.should.be.instanceof(Array);
        done();
      });
  });
});