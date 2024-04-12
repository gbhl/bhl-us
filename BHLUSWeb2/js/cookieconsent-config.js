import './cookieconsent.umd.js';

CookieConsent.run({
    disablePageInteraction: false,
    revision: 0,
    cookie: {
        expiresAfterDays: acceptType => { return acceptType === 'all' ? 365.25 : 182; }
    },
    guiOptions: {
        consentModal: {
            layout: 'cloud',
            position: 'bottom center'
        }
    },
    categories: {
        necessary: {
            enabled: true,
            readOnly: true
        },
        analytics: {
            readOnly: false,
            autoClear: {
                cookies: [
                    {
                        name: /^_ga/, // all starting with '_ga'
                        domain: location.hostname
                    },
                    {
                        name: /^_ga/,
                        domain: '.altmetric.com'
                    },
                    {
                        name: '_gid',
                    },
                    {
                        name: 'ACOOKIE',
                    }
                ],
                reloadPage: true
            },
            services: {
                ga: {
                    label: 'Google Analytics',
                    cookies: [
                        {
                            name: /^(_ga|_gid)/,
                            domain: location.hostname
                        },
                    ]
                },
                webtrends: {
                    label: 'WebTrends',
                    cookies: [
                        {
                            name: 'ACOOKIE'
                        }
                    ]
                },
                altmetric: {
                    label: "Altmetric",
                    cookies: [
                        {
                            name: /^_ga/,
                            domain: '.altmetric.com'
                        }
                    ]
                }
            }
        },
        ads: {}
    },
    language: {
        default: 'en',
        rtl: 'ar',
        autoDetect: 'browser',
        translations: {
            en: {
                consentModal: {
                    title: 'We value your privacy',
                    description: 'We use cookies to enhance your browsing experience and to analyze our traffic.  By clicking "Accept all" you consent to our use of cookies.',
                    acceptAllBtn: 'Accept all',
                    acceptNecessaryBtn: 'Reject all',
                    showPreferencesBtn: 'Customize',
                    footer: '<a href="https://www.si.edu/Privacy" target="_blank">Privacy Policy</a>',
                },
                preferencesModal: {
                    title: 'Manage cookie preferences',
                    acceptAllBtn: 'Accept all',
                    acceptNecessaryBtn: 'Reject all',
                    savePreferencesBtn: 'Accept current selection',
                    closeIconLabel: 'Close modal',
                    serviceCounterLabel: 'Service|Services',
                    sections: [{
                        title: 'Your Privacy Choices',
                        description: 'In this panel you can indicate your preferences related to our use of cookies. You may review and change your choices at any time. To allow or deny consent to specific activities, make your selections below and click "Accept Current Selection".  Alternately, click "Accept all" or "Reject all" to confirm or deny the use of all cookies that are not strictly necesesary.',
                        },
                        {
                            title: 'Strictly Necessary',
                            description: 'These cookies are essential for the proper functioning of the website and cannot be disabled.',
                            linkedCategory: 'necessary'
                        },
                        {
                            title: 'Performance and Analytics',
                            description: 'These cookies collect information about how you use our website. All of the data is anonymized and cannot be used to identify you.',
                            linkedCategory: 'analytics',
                            cookieTable: {
                                caption: 'Cookie table',
                                headers: {
                                    name: 'Cookie',
                                    domain: 'Domain',
                                    desc: 'Description'
                                },
                                body: [{
                                    name: '_ga',
                                    domain: location.hostname,
                                    desc: 'Tracking cookie used by Google Analytics to analyze web traffic.',
                                },
                                {
                                    name: '_gid',
                                    domain: location.hostname,
                                    desc: 'Tracking cookie used by Google Analytics to analyze web traffic.',
                                },
                                {
                                    name: 'ACOOKIE',
                                    domain: 'logs1.smithsonian.museum',
                                    desc: 'Tracking cookie used by WebTrends to analyze web traffic.'
                                },
                                {
                                    name: '_ga',
                                    domain: '.altmetric.com',
                                    desc: 'Tracking cookie used by Altmetric to analyze web traffic.',
                                },
                                ]
                            }
                        },
                        {
                            title: 'More information',
                            description: 'For questions about our policy on cookies and your choices, please <a href="https://www.biodiversitylibrary.org/contact">contact us</a>'
                        }
                    ]
                }
            }
        }
    }
});
