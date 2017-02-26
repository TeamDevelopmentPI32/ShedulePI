function buildConfig(env) {
    return require('./configs/' + env + '.config.js')({ env: env });
}

module.exports = buildConfig;