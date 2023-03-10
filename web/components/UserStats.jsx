import React from "react";

import { blockchains } from "../utilities/blockchains";

export default function UserStats({ wallet, blockchain, group }) {
  const [isMonth, setIsMonth] = React.useState();
  wallet.stats.walletAge > 24
    ? () => setIsMonth(false)
    : () => setIsMonth(true);

  const [coin, setCoin] = React.useState();
  React.useEffect(() => {
    for (let i = 0; i < blockchains.length; i++) {
      if (blockchains[i].slug === blockchain) {
        if (group === "eco") {
          setCoin(wallet.stats.token);
        } else {
          setCoin(blockchains[i].coin);
        }
      }
    }
  }, []);

  return (
    <>
      {!wallet.stats.noData ? (
        <div className="UserStats">
          <div className="balance">
            <div className="container">
              <span className="units">{coin}</span>
              {wallet.stats.nativeBalance > 1000
                ? Math.round(wallet.stats.nativeBalance / 10) / 100 + "k"
                : Math.round(wallet.stats.nativeBalance * 100) / 100}
            </div>
            {/* <div className="container">
              <span className="units">$</span>
              {wallet.stats.nativeBalanceUSD < 1000
                ? wallet.stats.nativeBalanceUSD
                : wallet.stats.nativeBalanceUSD < 1000000
                ? Math.floor((wallet.stats.nativeBalanceUSD / 1000) * 100) / 100 + "k"
                : wallet.stats.nativeBalanceUSD < 1000000000
                ? Math.floor((wallet.stats.nativeBalanceUSD / 1000000) * 100) / 100 +
                  "m"
                : Math.floor((wallet.stats.nativeBalanceUSD / 1000000000) * 100) /
                    100 +
                  "b"}
            </div> */}
            <span>Balance</span>
          </div>
          <div className="turnover">
            <div className="container">
              <span className="units">{coin}</span>
              {wallet.stats.walletTurnover > 1000
                ? Math.round(wallet.stats.walletTurnover / 10) / 100 + "k"
                : Math.round(wallet.stats.walletTurnover * 100) / 100}
            </div>
            <span>Wallet Turnover</span>
          </div>
          <div className="age">
            <div className="container">
              {isMonth && wallet.stats.walletAge}
              {isMonth && (
                <span>{wallet.stats.walletAge > 1 ? "months" : "month"}</span>
              )}
              {!isMonth && Math.floor(wallet.stats.walletAge / 12)}
              {!isMonth && <span className="units">y</span>}
              {!isMonth &&
                wallet.stats.walletAge -
                  Math.floor(wallet.stats.walletAge / 12) * 12 >
                  0 &&
                wallet.stats.walletAge -
                  Math.floor(wallet.stats.walletAge / 12) * 12}
              {!isMonth &&
                wallet.stats.walletAge -
                  Math.floor(wallet.stats.walletAge / 12) * 12 >
                  0 && <span className="units">mo</span>}
            </div>

            <span>Wallet Age</span>
          </div>
        </div>
      ) : (
        "No data to show"
      )}
    </>
  );
}
