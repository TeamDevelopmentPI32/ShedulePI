const path = require('path');
const webpack = require('webpack');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const ExtractTextPlugin = require('extract-text-webpack-plugin');

module.exports = function() {
    return {
        
        entry: {
            'app': './src/app.jsx',
            'vendor': './src/vendor.js'
        },

        resolve: {
            extensions: ['.js', '.jsx', '.css', '.scss', '.sass'],
        },

        module: {
            rules: [

                {
                    test: /.(js|jsx)$/,
                    exclude: /node_modules/,
                    loader: 'babel-loader',
                    query: {
                        presets: ['react', 'es2015', 'stage-2']
                    }
                },

                {
                    test: /\.css$/,
                    exclude: /node_modules/,
                    loader: 'css-loader'
                },

                {
                    test: /\.(sass|scss)$/,
                    exclude: /node_modules/,
                    use: ExtractTextPlugin.extract({
                        use: [
                            { loader: 'css-loader' },
                            { loader: 'sass-loader' },
                        ]
                    }),
                },

                {
                   test: /\.(jpg|png|gif)$/,
                   loader: 'file-loader'
                },

                {
                   test: /\.(woff|woff2|eot|ttf|svg)$/,
                   loader: 'url-loader?limit=100000'
                }

            ]
        },

        plugins: [

                new webpack.optimize.CommonsChunkPlugin({
                    name: ['vendor'].reverse()
                }),
                
                new HtmlWebpackPlugin({
                    template: './src/index.html'
                }),

            ]
    }
}