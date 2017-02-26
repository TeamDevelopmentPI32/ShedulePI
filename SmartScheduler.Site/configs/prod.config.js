const path = require('path');
const webpack = require('webpack');
const webpackMerge = require('webpack-merge');
const commonConfig = require('./base.config.js');
const ExtractTextPlugin = require('extract-text-webpack-plugin');

module.exports = function(env) {
    return webpackMerge(commonConfig(), {

        output: {
            path: path.join(__dirname, '../dist'),
            filename: '[name].[chunkhash].js',
            publicPath: '/',
            sourceMapFilename: '[name].[chunkhash].map' 
        },

        plugins: [

            new webpack.LoaderOptionsPlugin({
                minimize: true,
                debug: false
            }),

            new webpack.DefinePlugin({
                'process.env': {
                    'NODE_ENV': JSON.stringify('production')
                }
            }),

            new webpack.optimize.UglifyJsPlugin({
                beautify: false,
                mangle: {
                    screw_ie8: true,
                    keep_fnames: true
                },
                compress: {
                    screw_ie8: true
                },
                comments: false
            }),

            new ExtractTextPlugin('styles.[chunkhash].css')

        ]

    });
}