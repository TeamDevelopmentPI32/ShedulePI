const path = require('path');
const webpack = require('webpack');
const webpackMerge = require('webpack-merge');
const commonConfig = require('./base.config.js');
const ExtractTextPlugin = require('extract-text-webpack-plugin');


module.exports = function(env) {
    return webpackMerge(commonConfig(), {
    devtool: 'eval',

    output: {
        path: path.join(__dirname, '../build'),
        filename: '[name].js',
        publicPath: '/',
        sourceMapFilename: '[name].map' 
    },

    devServer: {
        port: 7777,
        host: 'localhost',
        historyApiFallback: true,
        noInfo: false,
        stats: 'minimal',
        publicPath: '/'
    },

    plugins: [
        new ExtractTextPlugin('styles.css'),
    ]

  });
}