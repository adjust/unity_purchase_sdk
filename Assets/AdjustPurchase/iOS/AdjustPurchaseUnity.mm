#import "AdjustPurchaseUnity.h"

@implementation AdjustPurchaseUnity

static char* adjustPurchaseSceneName = nil;
static id adjustPurchaseUnityInstance = nil;

- (id)init {
    self = [super init];
    return self;
}

- (void)adjustVerificationUpdate:(ADJPVerificationInfo *)info {
    NSMutableDictionary *dictVerificationInfo = [[NSMutableDictionary alloc] init];

    [dictVerificationInfo setObject:info.message forKey:@"message"];
    [dictVerificationInfo setObject:[info getVerificationStateAsString] forKey:@"verificationState"];
    [dictVerificationInfo setObject:[NSString stringWithFormat:@"%d", info.statusCode] forKey:@"statusCode"];

    NSData *dataVerificationInfo = [NSJSONSerialization dataWithJSONObject:dictVerificationInfo options:0 error:nil];
    NSString *stringVerificationInfo = [[NSString alloc] initWithBytes:[dataVerificationInfo bytes]
                                                                length:[dataVerificationInfo length]
                                                                encoding:NSUTF8StringEncoding];

    const char* charArrayVerificationInfo = [stringVerificationInfo UTF8String];

    UnitySendMessage(adjustPurchaseSceneName, "GetNativeVerificationInfo", charArrayVerificationInfo);
}

@end

extern "C"
{
    void _AdjustPurchaseInit(const char* appToken, const char* environment, const char* sdkPrefix, int logLevel) {
        NSString *stringSdkPrefix = [NSString stringWithUTF8String:sdkPrefix];
        NSString *stringAppToken = [NSString stringWithUTF8String:appToken];
        NSString *stringEnvironment = [NSString stringWithUTF8String:environment];

        ADJPConfig *config = [[ADJPConfig alloc] initWithAppToken:stringAppToken andEnvironment:stringEnvironment];

        if (logLevel != -1) {
            [config setLogLevel:(ADJPLogLevel)logLevel];
        }
        if (stringSdkPrefix != nil && [stringSdkPrefix length] > 0) {
            [config setSdkPrefix:stringSdkPrefix];
        }

        adjustPurchaseUnityInstance = [[AdjustPurchaseUnity alloc] init];
        [AdjustPurchase init:config];
    }

    void _AdjustPurchaseVerifyPurchase(const char* receipt, const char* transactionId, const char* productId, const char* sceneName) {
        NSString *stringReceipt = nil;
        NSString *stringTransactionId = nil;
        NSString *stringProducId = nil;
        NSString *stringSceneName = [NSString stringWithUTF8String:sceneName];

        if (sceneName != NULL && [stringSceneName length] > 0) {
            adjustPurchaseSceneName = strdup(sceneName);
        }
        if (receipt != NULL) {
            stringReceipt = [NSString stringWithUTF8String:receipt];
        }
        if (transactionId != NULL) {
            stringTransactionId = [NSString stringWithUTF8String:transactionId];
        }
        if (productId != NULL) {
            stringProducId = [NSString stringWithUTF8String:productId];
        }

        [AdjustPurchase verifyPurchase:[stringReceipt dataUsingEncoding:NSUTF8StringEncoding]
                      forTransactionId:stringTransactionId
                             productId:stringProducId
                     withResponseBlock:^(ADJPVerificationInfo *info) {
                         [adjustPurchaseUnityInstance adjustVerificationUpdate:info];
                     }];
    }
}
