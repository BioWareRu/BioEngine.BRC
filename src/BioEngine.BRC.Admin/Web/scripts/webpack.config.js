const path = require('path');
const webpack = require('webpack');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const CssoWebpackPlugin = require('csso-webpack-plugin').default;
const TerserPlugin = require('terser-webpack-plugin');

module.exports = {
    entry: path.resolve(__dirname, 'src', 'index.js'),
    output: {
        filename: 'index.js',
        path: path.resolve(__dirname, '..', '..', 'wwwroot', 'dist'),
    },
    plugins: [
        new webpack.ProvidePlugin({
            $: 'jquery',
            jQuery: 'jquery',
            'window.jQuery': 'jquery',
            'global.jQuery': 'jquery',
        }),
        new MiniCssExtractPlugin({
            filename: "styles.css",
            cssProcessorOptions: {
                safe: true,
                discardComments: {
                    removeAll: true,
                },
            },
        }),
        new CssoWebpackPlugin()
    ],
    module: {
        rules: [
            {
                test: /\.(sa|sc)ss$/,
                use: [MiniCssExtractPlugin.loader, 'css-loader', 'sass-loader']
            },
            {
                test: /\.(png|jpe?g|gif|svg|eot|ttf|woff|woff2)$/i,
                loader: 'file-loader',
                options: {
                    name: '[path][name].[ext]?[contenthash]'
                }
            }
        ]
    },
    optimization: {
        minimize: true,
        minimizer: [
            new TerserPlugin({
                sourceMap: false,
                terserOptions: {
                    compress: {
                        drop_console: true, // will remove console.logs from your files
                    },
                },
            }),
        ],
    }
};